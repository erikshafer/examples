using System;
using GraphQL.Types;
using StarWars.Types;

namespace StarWars
{
    public class StarWarsQuery : ObjectGraphType<object>
    {
        public StarWarsQuery(StarWarsData data)
        {
            Name = "Query";

            // hero
            Field<CharacterInterface>("hero", resolve: context => data.GetDroidByIdAsync("3"));

            // human
            Field<HumanType>(
                "human",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the human" }
                ),
                resolve: context => data.GetHumanByIdAsync(context.GetArgument<string>("id"))
            );

            // droid
            Func<ResolveFieldContext, string, object> func = (context, id) => data.GetDroidByIdAsync(id);
            FieldDelegate<DroidType>(
                "droid",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the droid" }
                ),
                resolve: func
            );

            // getAllDroids
            Field<ListGraphType<DroidType>>(
                "getAllDroids",
                resolve: context => data.GetAllDroidsAsync()
            );

            // getAllDroidsWithFriends
            Field<ListGraphType<DroidType>>(
                "getAllDroidsWithFriends",
                resolve: context => data.GetAllDroidsWithFriendsAsync()
            );

        }
    }
}

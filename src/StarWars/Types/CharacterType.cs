using GraphQL.Types;

namespace StarWars.Types
{
    public class CharacterType : ObjectGraphType<GenericCharacter>
    {
        public CharacterType(StarWarsData data)
        {
            Name = "Character";
            Description = "A character in the Star Wars universe.";

            Field(c => c.Id).Description("The id of the character.");
            Field(c => c.Name, nullable: true).Description("The name of the character.");

            Field<ListGraphType<CharacterInterface>>(
                "friends",
                resolve: context => data.GetFriends(context.Source)
            );
            Field<ListGraphType<EpisodeEnum>>("appearsIn", "Which movie they appear in.");
            
            Interface<CharacterInterface>();
        }
    }
}

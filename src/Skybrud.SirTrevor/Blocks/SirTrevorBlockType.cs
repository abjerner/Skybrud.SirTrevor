namespace Skybrud.SirTrevor.Blocks {
    
    public class SirTrevorBlockType {

        public string Id { get; private set; }
        public string Name { get; private set; }
        public bool IsNative { get; private set; }

        public SirTrevorBlockType(string id, string name) {
            Id = id;
            Name = name;
        }

        internal SirTrevorBlockType(string id, string name, bool isNative) {
            Id = id;
            Name = name;
            IsNative = isNative;
        }

        public static SirTrevorBlockType[] GetBlockTypes() {

            // TODO: Read custom block types from a config file or similar

            return new[] {
                new SirTrevorBlockType("Heading", "Heading", true),
                new SirTrevorBlockType("Image", "Image", true),
                new SirTrevorBlockType("List", "List", true),
                new SirTrevorBlockType("Quote", "Quote", true),
                new SirTrevorBlockType("Text", "Text", true),
                new SirTrevorBlockType("Tweet", "Tweet", true),
                new SirTrevorBlockType("Video", "Video", true)
            };

        }

    }

}
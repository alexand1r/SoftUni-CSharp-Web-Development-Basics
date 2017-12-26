namespace SocialNetwork
{
    using System.Linq;

    public static class TagTransformer
    {
        public static string Transform(string tag)
        {
            var newTag = string.Empty;

            if (tag[1] != '#')
                newTag = "#" + tag;
            else
                newTag = tag;

            newTag = newTag.Replace(" ", "");
            newTag = newTag.Length > 20 ? newTag.Substring(0, 20) : newTag;

            return newTag;
        }
    }
}

namespace SocialNetwork.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class TagAttribute : ValidationAttribute
    {
        public override bool IsValid(object tagValue)
        {
            string tag = tagValue.ToString();

            if (!tag.Contains("#"))
            {
                return false;
            }
            if (tag.Contains(" ") || tag.Contains("\t"))
            {
                return false;
            }
            if (tag.Length > 20)
            {
                return false;
            }

            return true;
        }
    }
}
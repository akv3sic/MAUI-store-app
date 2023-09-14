namespace MauiStoreApp.Models
{
    public class Category
    {
        public string Name { get; set; }

        public string ImageName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    return null;
                }

                string formattedName = Name.Replace(" ", "_").Replace("'", "").ToLower();
                return formattedName + ".jpg";
            }
        }
    }
}

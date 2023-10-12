using System.Collections;
using System.Reflection;

namespace LogicitApp.Shared.Attributes
{
    public class ValidationAttribute : Attribute
    {
        public string PropertyLocalizedName { get; private set; }
        public Type Type { get; private set; }

        public Func<object, bool> ValidationRule
        {
            get
            {
                if (Type == typeof(string))
                    return str => !string.IsNullOrEmpty(str as string);
                else if (Type.GetInterface(nameof(ICollection)) != null)
                    return x => ((ICollection)x).Count > 0;

                return obj => obj != null;
            }
        }

        public ValidationAttribute(string propertyLocalizedName, Type propertyType)
        {
            PropertyLocalizedName = propertyLocalizedName;
            Type = propertyType;
        }

        public static List<string> Validate(object instance)
        {
            var notValidProps = new List<string>();

            var p = instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.GetCustomAttributes(typeof(ValidationAttribute), false).Any());

            foreach (var item in p)
            {
                var attr = item.GetCustomAttribute<ValidationAttribute>();
                var isValid = attr.ValidationRule.Invoke(item.GetValue(instance));
                if (isValid != true)
                    notValidProps.Add(attr.PropertyLocalizedName);
            }

            return notValidProps;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Life30.Helpers
{
    public class MessageAttribute : Attribute
    {
        public string Message { get; private set; }

        public MessageAttribute(string _message)
        {
            Message = _message;
        }
    }
    public static class EnumerationHelper
    {
        public static string GetDescription(this Enum value)
        {
            var descriptionAttribute = (DescriptionAttribute)value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(false)
                .Where(a => a is DescriptionAttribute)
                .FirstOrDefault();

            return descriptionAttribute != null ? descriptionAttribute.Description : value.ToString();
        }

        public static string GetMessage(this Enum value)
        {
            var descriptionAttribute = (MessageAttribute)value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(false)
                .Where(a => a is MessageAttribute)
                .FirstOrDefault();

            return descriptionAttribute != null ? descriptionAttribute.Message : value.ToString();
        }

        public static string GetNamebyDescription(object value, string description)
        {
            string Name = null;

            foreach (FieldInfo childinfo in value.GetType().GetFields().ToList())
            {
                DescriptionAttribute descriptionAttribute = childinfo.GetCustomAttributes(true).Where(x => x is DescriptionAttribute).FirstOrDefault() as DescriptionAttribute;

                if (descriptionAttribute != null && descriptionAttribute.Description == description)
                {
                    Name = ((MemberInfo)(childinfo)).Name;
                }

            }

            return Name != null ? Name.ToString() : description;
        }

        public static string GetNamebyDescription(Type t, string description)
        {
            string Name = null;

            foreach (FieldInfo childinfo in t.GetFields().ToList())
            {
                DescriptionAttribute descriptionAttribute = childinfo.GetCustomAttributes(true).Where(x => x is DescriptionAttribute).FirstOrDefault() as DescriptionAttribute;

                if (descriptionAttribute != null && descriptionAttribute.Description.Equals(description, StringComparison.InvariantCultureIgnoreCase))
                {
                    Name = ((MemberInfo)(childinfo)).Name;
                }

            }

            return Name != null ? Name.ToString() : description;
        }

        public static object GetEnumbyDescription(object value, string description)
        {
            object returnObject = null;

            foreach (FieldInfo childinfo in value.GetType().GetFields().ToList())
            {
                DescriptionAttribute descriptionAttribute = childinfo.GetCustomAttributes(true).Where(x => x is DescriptionAttribute).FirstOrDefault() as DescriptionAttribute;

                if (descriptionAttribute != null && descriptionAttribute.Description == description)
                {
                    returnObject = ((MemberInfo)(childinfo));
                }

            }

            return returnObject != null ? returnObject : description;
        }

        public static T Parse<T>(string status)
        {
            string unformatedStatus = EnumerationHelper.UnformatStatus(status);

            foreach (T type in Enum.GetValues(typeof(T)))
            {
                Enum enumerate = type as Enum;
                if (UnformatStatus(GetDescription(enumerate)) == unformatedStatus) { return type; }
            }

            foreach (T type in Enum.GetValues(typeof(T)))
            {
                Enum enumerate = type as Enum;
                if (GetDescription(enumerate) == "None") { return type; }
            }

            throw new FormatException(string.Format("The {0} is not as expected. (Given: {1})", typeof(T).ToString().Split('.').LastOrDefault(), status));
        }

        public static string UnformatStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) { return ""; }
            return status.ToLower().Replace(" ", "").Replace("_", "");
        }
    }
}

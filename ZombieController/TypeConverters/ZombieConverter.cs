using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Globalization;
using ZombieControl.Model;

namespace ZombieControl.TypeConverters
{
    public class ZombieConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    Zombie zombie = JsonConvert.DeserializeObject<Zombie>((string)value);
                    return zombie;
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    base.ConvertFrom(context, culture, value);
                }
               
            }
            return base.ConvertFrom(context, culture, value);
        }

    }
}

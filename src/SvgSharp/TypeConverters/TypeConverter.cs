using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Svg
{
    public abstract class BaseConverter<TTarget, TArgs> : BaseConverter<TTarget>
    {
        public TArgs Args { get; private set; }

        internal override void SetArgs(object args)
        {
            Args = (TArgs)args;
        }
    }

    public abstract class BaseConverter<TTarget> : BaseConverter
    {
        public override Type ConvertableType => typeof(TTarget);

        public override string ConvertFrom(object value, SvgDocument context)
        {
            return this.Convert((TTarget)value, context);
        }

        public override object ConvertTo(string value, SvgDocument context)
        {
            return this.Convert(value, context);
        }

        public abstract TTarget Convert(string value, SvgDocument context);

        public abstract string Convert(TTarget value, SvgDocument context);
    }

    public abstract class SimpleBaseConverter<TTarget, TArgs> : SimpleBaseConverter<TTarget>
    {
        public TArgs Args { get; private set; }

        internal override void SetArgs(object args)
        {
            Args = (TArgs)args;
        }
    }

    public abstract class SimpleBaseConverter<TTarget> : BaseConverter<TTarget>
    {
        public override string Convert(TTarget value, SvgDocument context)
        {
            return this.Convert(value);
        }

        public override TTarget Convert(string value, SvgDocument context)
        {
            return this.Convert(value);
        }

        public abstract TTarget Convert(string value);

        public abstract string Convert(TTarget value);
    }

    public abstract class BaseConverter
    {
        internal virtual void SetArgs(object args)
        {
            //noop
        }

        public abstract Type ConvertableType { get; }

        public abstract string ConvertFrom(object value, SvgDocument context);
        public abstract object ConvertTo(string value, SvgDocument context);
    }
    
    public class TypeConverterAttribute : Attribute
    {
        public TypeConverterAttribute(Type type)
            :this (type, null)
        {

        }

        public TypeConverterAttribute(Type type, object args)
        {
            this.Type = type;
            this.TypeConverter = Activator.CreateInstance(type) as BaseConverter;
            if(this.TypeConverter == null)
            {
                throw new ArgumentException($"{type.FullName} doesn't inherit from BaseConverter.", nameof(type));
            }
                if (args != null)
            {
                this.TypeConverter?.SetArgs(args);
            }
        }

        public Type Type { get; private set; }
        public BaseConverter TypeConverter { get; private set; }
    }
}

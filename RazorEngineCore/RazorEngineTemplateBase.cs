using System;
using System.Text;
using System.Threading.Tasks;

namespace RazorEngineCore
{
    public abstract class RazorEngineTemplateBase : IRazorEngineTemplate
    {
        private readonly StringBuilder _stringBuilder = new();

        private string? _attributeSuffix;

        public dynamic? Model { get; set; }

        public Action Breakpoint { get; set; } = () => { };

        public virtual void WriteLiteral(string? literal = null)
        {
            this._stringBuilder.Append(literal);
        }

        public virtual void Write(object? obj = null)
        {
            this._stringBuilder.Append(obj);
        }

        public virtual void BeginWriteAttribute(string name, string prefix, int prefixOffset, string suffix, int suffixOffset,  int attributeValuesCount)
        {
            this._attributeSuffix = suffix;
            this._stringBuilder.Append(prefix);
        }

        public virtual void WriteAttributeValue(string prefix, int prefixOffset, object value, int valueOffset, int valueLength, bool isLiteral)
        {
            this._stringBuilder.Append(prefix);
            this._stringBuilder.Append(value);
        }

        public virtual void EndWriteAttribute()
        {
            this._stringBuilder.Append(this._attributeSuffix);
            this._attributeSuffix = null;
        }

        public virtual Task ExecuteAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task<string> ResultAsync()
        {
	        return Task.FromResult(this._stringBuilder.ToString());
        }
	}
}
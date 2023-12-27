using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace RazorEngineCore
{
    public class RazorEngineCompilationException : RazorEngineException
    {
        public List<Diagnostic>? Errors { get; set; }
        
        public string? GeneratedCode { get; set; }

        public override string Message
        {
            get
            {
                string errors = string.Join(Environment.NewLine, this.Errors?.Where(w => 
                    w.IsWarningAsError || w.Severity is DiagnosticSeverity.Error) ?? new List<Diagnostic>());

                return "Unable to compile template: " + errors;
            }
        }
        
        public RazorEngineCompilationException()
        {
        }

        public RazorEngineCompilationException(Exception innerException) : base(null, innerException)
        {
        }
    }
}
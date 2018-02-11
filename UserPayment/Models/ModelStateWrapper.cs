using System;
using System.Web.Mvc;

namespace UserPayment.Models
{
    public class ModelStateWrapper : IValidationDictionary
    {
        private readonly ModelStateDictionary _modelState;

        public ModelStateWrapper(ModelStateDictionary modelState)
        {
            _modelState = modelState;
        }

        public ModelStateWrapper()
        {
            _modelState = new ModelStateDictionary();
        }

        public bool IsValid
        {
            get
            {
                return _modelState.IsValid;
            }
        }

        public void AddError(string key, string errorMessage)
        {
            _modelState.AddModelError(key, errorMessage);
        }
    }
}
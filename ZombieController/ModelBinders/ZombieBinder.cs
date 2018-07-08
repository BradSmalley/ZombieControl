using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using ZombieControl.Model;

namespace ZombieControl.ModelBinders
{
    public class ZombieBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var valueAsString = valueProviderResult.FirstValue;

            var result = JsonConvert.DeserializeObject<Zombie>(valueAsString);
            bindingContext.Result = ModelBindingResult.Success(result);

            return Task.CompletedTask;
        }
    }
}

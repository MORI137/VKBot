using Microsoft.AspNetCore.Mvc.ModelBinding;
using VkNet.Model;

namespace VKBot
{
    public class VkMessageBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(Message))
            {
                return new VkMessageModelBinder();
            }

            return null;
        }
    }
}

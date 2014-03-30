namespace Recognized.Reflection
{
    using System;


    public class ModelInfo
    {
        public readonly Type ModelType;

        /// <summary>
        /// The container type that holds this model
        /// </summary>
        public readonly Type ContainerType;

        public ModelInfo(Type modelType, Type containerType)
        {
            ModelType = modelType;
            ContainerType = containerType;
        }
    }
}
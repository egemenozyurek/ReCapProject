using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validaton.FluentValidation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            if (!validatorType.IsAssignableFrom(typeof(IValidator)))
            {
                throw new System.Exception("It is not a validator.");
            }
            _validatorType = validatorType;
        }

        protected override void OnBefore(IInvocation invocation)
        {

            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entitiesOfMethod = invocation.Method.GetParameters();

            foreach (var entity in entitiesOfMethod)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}

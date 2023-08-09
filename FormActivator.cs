using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Phoenix.Core;
using Phoenix.Extentions;

namespace Phoenix
{
    internal static class FormActivator
    {
        internal static Dictionary<string, FormConstructorDelegate> _formConstructorDelegates
            = new Dictionary<string, FormConstructorDelegate>();

        internal static void AddFormConstructor(string name, FormConstructorDelegate constructorDelegate)
        {
            _formConstructorDelegates.Add(name, constructorDelegate);
        }

        internal delegate object FormConstructorDelegate();

        internal static FormConstructorDelegate CreateFormConstructor(Type type, params Type[] parameters)
        {
            var constructorInfo = type.GetConstructor(parameters);

            var body = Expression.New(constructorInfo);

            var constructor = Expression.Lambda<FormConstructorDelegate>(body);

            return constructor.Compile();
        }

        internal static void TryActivateForm<T>(string formName) where T : PhoenixForm
        {
            if (!PContainer.CheckExistsForm(formName))
            {
                var formConstructor = _formConstructorDelegates.Get(formName);
                PhoenixForm form = formConstructor() as PhoenixForm;
                ActivateForm(form);

                _formConstructorDelegates.Remove(formName);
            }
        }

        internal static void ActivateForm(PhoenixForm form)
        {
            PContainer.Append(form.Name, form);
            form.InitializeEvents();
            form.EnableFormHiding();
        }

        internal static void ProtectsAndValidatesPassedTypes(Type formType)
        {
            if (!formType.IsClass || formType.BaseType.Name != typeof(PhoenixForm).Name)
            {
                throw new PhoenixException(
                    "The passed argument is not a type of the PhoenixForm class!",
                    new ArgumentException()
                );
            }
        }
    }
}

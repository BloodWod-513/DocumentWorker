using System.Reflection;
using Autofac;
using Autofac.Core;


namespace AutofacWrapper
{
    public abstract class AutofacModuleBuilder
    {
        private ContainerBuilder _builder;

        protected AutofacModuleBuilder(ContainerBuilder builder)
        {
            _builder = builder;
        }
        private AutofacModuleBuilder()
        { }


        /// <summary>
        /// Тут нужно описывать связки сервисов и интрефейсов
        /// </summary>
        public abstract void Registers(ModuleType moduleType);

        public ContainerBuilder GetBuilder()
        {
            return _builder;
        }

        /// <summary>
        /// Простое сопоставление реализации интерфейсу
        /// </summary>
        /// <typeparam name="TClass">Реализация</typeparam>
        /// <typeparam name="TInterface">Интерфейс</typeparam>
        /// <param name="moduleType">Тип scope</param>
        protected void SimpleRegister<TClass, TInterface>(ModuleType moduleType) where TClass : TInterface
        {
            _builder.RegisterType<TClass>().As<TInterface>().SetLifeTime(moduleType);
        }

        /// <summary>
        /// Регистрация типа
        /// </summary>
        /// <typeparam name="TClass">Реализация</typeparam>
        /// <param name="moduleType">Тип scope</param>
        protected void SimpleRegister<TClass>(ModuleType moduleType) where TClass : class
        {
            _builder.RegisterType<TClass>().SetLifeTime(moduleType);
        }

        /// <summary>
        /// Регистрация типа
        /// </summary>
        /// <param name="type"></param>
        /// <param name="moduleType">Тип scope</param>
        protected void SimpleRegister(Type type, ModuleType moduleType)
        {
            _builder.RegisterType(type).SetLifeTime(moduleType);
        }

        /// <summary>
        /// Простое сопоставление реализации интерфейсу
        /// </summary>
        /// <param name="moduleType">Тип scope</param>
        /// <param name="class">Тип регистрируемого компонента</param>
        /// <param name="interface">Интерфейс регистрируемого сервиса</param>
        protected void SimpleRegister(Type @class, Type @interface, ModuleType moduleType)
        {
            _builder.RegisterType(@class).As(@interface).SetLifeTime(moduleType);
        }

        /// <summary>
        /// Регистрация типа с ключем
        /// </summary>
        /// <typeparam name="TClass">Реализация</typeparam>
        /// <typeparam name="TInterface">Интерфейс</typeparam>
        /// <param name="moduleType">Тип scope</param>
        /// <param name="serviceKey">Ключ</param>
        protected void SimpleRegister<TClass, TInterface>(ModuleType moduleType, object serviceKey) where TClass : TInterface
        {
            _builder.RegisterType<TClass>().Keyed<TInterface>(serviceKey).SetLifeTime(moduleType);
        }

        /// <summary>
        /// Регистрация экземпляра сервиса
        /// </summary>
        /// <typeparam name="TInterface">Интерфейс сервиса</typeparam>
        /// <param name="instance">Экземпляр класса реализующего интерфейс сервиса</param>
        protected void RegisterInstance<TInterface>(TInterface instance) where TInterface : class
        {
            _builder.RegisterInstance(instance).As<TInterface>();
        }

        /// <summary>
        /// Регистрация generic
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="interfaceType"></param>
        /// <param name="moduleType"></param>
        protected void RegisterGeneric(Type classType, Type interfaceType, ModuleType moduleType)
        {
            _builder.RegisterGeneric(classType).As(interfaceType).SetLifeTime(moduleType);
        }

        /// <summary>
        /// Регистрация generic с параметром
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="interfaceType"></param>
        /// <param name="moduleType"></param>
        /// <param name="parameter"></param>
        protected void RegisterGeneric(Type classType, Type interfaceType, ModuleType moduleType, Parameter parameter)
        {
            var parameters = new List<Parameter>() { parameter };
            RegisterGeneric(classType, interfaceType, moduleType, parameters);
        }

        /// <summary>
        /// Регистрация generic с параметрами
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="interfaceType"></param>
        /// <param name="moduleType"></param>
        /// <param name="parameters"></param>
        protected void RegisterGeneric(Type classType, Type interfaceType, ModuleType moduleType, IEnumerable<Parameter> parameters)
        {
            _builder.RegisterGeneric(classType).As(interfaceType).WithParameters(parameters).SetLifeTime(moduleType);
        }
    
        /// <summary>
        /// Регистрация AutofacModuleBuilder
        /// </summary>
        protected void RegisterModuleBuilder(AutofacModuleBuilder moduleBuilder, ModuleType moduleType)
        {
            moduleBuilder.Registers(moduleType);
        }

        /// <summary>
        /// Регистрация AutofacModuleBuilder
        /// </summary>
        protected void RegisterModuleBuilder<TAutofacModuleBuilder>(ModuleType moduleType) where TAutofacModuleBuilder : AutofacModuleBuilder, new()
        {
            var moduleBuilder = new TAutofacModuleBuilder()
            {
                _builder = _builder
            };
            RegisterModuleBuilder(moduleBuilder, moduleType);
        }

        /// <summary>
        /// Регистрация типа через делегат
        /// </summary>
        protected void RegisterDelegate<T>(Func<IComponentContext, T> registerDelegate, ModuleType moduleType)
        {
            _builder.Register(registerDelegate).SetLifeTime(moduleType);
        }
    }

}
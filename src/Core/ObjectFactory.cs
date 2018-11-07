#region imports

using System;

#endregion imports

namespace Core
{
    /// <summary>
    ///
    /// </summary>
    public static class ObjectFactory
    {
        /// <summary>
        ///
        /// </summary>
        static ObjectFactory()
        {
            StructureMap.ObjectFactory.Initialize(action =>
            {
                action.PullConfigurationFromAppConfig = true;
            });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static object GetInstance(string typeName)
        {
            Type type = Type.GetType(typeName);

            return ObjectFactory.GetInstance(type);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pluginType"></param>
        /// <returns></returns>
        public static object GetInstance(Type pluginType)
        {
            object returnObject = null;

            try
            {
                returnObject = StructureMap.ObjectFactory.GetInstance(pluginType);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetInstance<T>()
        {
            T returnObject = default(T);

            try
            {
                returnObject = StructureMap.ObjectFactory.GetInstance<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="pluginName"></param>
        /// <returns></returns>
        public static object GetInstance(string typeName, string pluginName)
        {
            Type type = Type.GetType(typeName);

            return ObjectFactory.GetInstance(type, pluginName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pluginType"></param>
        /// <param name="pluginName"></param>
        /// <returns></returns>
        public static object GetInstance(Type pluginType, string pluginName)
        {
            object returnObject = null;

            try
            {
                returnObject = StructureMap.ObjectFactory.GetNamedInstance(pluginType, pluginName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pluginName"></param>
        /// <returns></returns>
        public static object GetInstance<T>(string pluginName)
        {
            T returnObject = default(T);

            try
            {
                returnObject = StructureMap.ObjectFactory.GetNamedInstance<T>(pluginName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnObject;
        }

        /// <summary>
        /// Creates an instance of the type specified, using the
        /// default constructor.
        /// </summary>
        /// <param name="type">
        /// The preferred type.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        public static object CreateObject(Type type)
        {
            return ObjectFactory.CreateObject(type, null);
        }

        /// <summary>
        /// Creates an instance of the type specified, using the
        /// most suitable constructor.
        /// </summary>
        /// <param name="type">
        /// The preferred type.
        /// </param>
        /// <param name="args">
        /// The arguments to be passed to the constructor.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        public static object CreateObject(Type type, params object[] args)
        {
            object returnObject = null;
            
            // check whether constructor arguments have not been provided
            if (args == null)
            {
                // attempt to create the instance using the default constructor
                returnObject = Activator.CreateInstance(type);
            }
            else
            {
                // attempt to create the instance using the most suitable constructor
                returnObject = Activator.CreateInstance(type, args);
            }
            
            return returnObject;
        }

        /// <summary>
        /// Creates an instance of the type whose name is specified, using the
        /// default constructor.
        /// </summary>
        /// <param name="typeName">
        /// The name of the preferred type.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        public static object CreateObject(string typeName)
        {
            object[] args = null;

            return ObjectFactory.CreateObject(typeName, args);
        }

        /// <summary>
        /// Creates an instance of the type whose name is specified, using the
        /// most suitable constructor.
        /// </summary>
        /// <param name="typeName">
        /// The name of the preferred type.
        /// </param>
        /// <param name="args">
        /// The arguments to be passed to the constructor.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        public static object CreateObject(string typeName, params object[] args)
        {
            object returnObject = null;


            Type type = Type.GetType(typeName);

            // check whether constructor arguments have not been provided
            if (args == null)
            {
                // attempt to create the instance using the default constructor
                returnObject = Activator.CreateInstance(type);
            }
            else
            {
                // attempt to create the instance using the most suitable constructor
                returnObject = Activator.CreateInstance(type, args);
            }


            return returnObject;
        }

        /// <summary>
        /// Creates an instance of the type whose name is specified, using the
        /// named assembly and default constructor.
        /// </summary>
        /// <param name="assemblyName">
        /// The name of the assembly where the type named <paramref name="typeName"/> is sought.
        /// If <paramref name="assemblyName"/> is <c>null</c>, the executing assembly is searched.
        /// </param>
        /// <param name="typeName">
        /// The name of the preferred type.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        /// <exception cref="ReflectionException">
        /// An error occurs while attempting to reflect on a type.
        /// </exception>
        public static object CreateObject(string assemblyName, string typeName)
        {
            object returnObject = null;

            try
            {
                System.Runtime.Remoting.ObjectHandle handle =
                    Activator.CreateInstance(assemblyName, typeName);

                returnObject = handle.Unwrap();
            }
            catch (Exception)
            {
                //throw new ReflectionException(ex.Message, ex);
            }

            return returnObject;
        }
    }
}
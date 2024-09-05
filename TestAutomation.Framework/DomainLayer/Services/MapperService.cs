using System.Collections.Generic;
using Intertech.TestAutomation.Framework.DomainLayer.POMBase;
using Intertech.TestAutomation.Framework.DomainLayer.Contracts;

namespace Intertech.TestAutomation.Framework.DomainLayer.Services
{
    public static class MapperService
    {
        private static readonly Dictionary<Test, IList<IMapper>> repositoryMap =
            new Dictionary<Test, IList<IMapper>>();

        public static void Add<T>(Test parent, object key, object value) where T: new()
        {
            IList<IMapper> repositoryList = new List<IMapper>();
            IMapper repository = null;
            int listIndex = -1;

            if (repositoryMap.ContainsKey(parent))
            {
                repositoryList = repositoryMap[parent];
            }

            repository = GetMatchedMap(repositoryList, (IMapper)new T());

            listIndex = repositoryList.IndexOf(repository);
            
            repository = repository ?? (IMapper)new T();

            repository.Add(key, value);

            if (listIndex == -1)
            {
                repositoryList.Add(repository);
            }
            else
            {
                repositoryList[listIndex] = repository;
            }
            
            repositoryMap[parent] = repositoryList;
        }

        public static IMapper Get<T>(Test parent) where T : new()
        {
            if ( ! repositoryMap.ContainsKey(parent))
            {
                repositoryMap[parent] = new List<IMapper>();
            }

            return GetMatchedMap(repositoryMap[parent], (IMapper)new T());
        }

        private static IMapper GetMatchedMap(IList<IMapper> repositoryList, IMapper T)
        {
            foreach (IMapper repo in repositoryList)
            {
                if (repo.GetType().Equals(T.GetType()))
                {
                    return repo;
                }
            }

            return T;
        }
        
    }
}

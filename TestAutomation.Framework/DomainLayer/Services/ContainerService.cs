using System.Collections.Generic;
using TestAutomation.Framework.DomainLayer.POMBase;
using TestAutomation.Framework.DomainLayer.Contracts;
using System;
using System.Collections.Concurrent;

namespace TestAutomation.Framework.DomainLayer.Services {
    public static class ContainerService {
        private static readonly ConcurrentDictionary<Test, IList<IContainer>> repositoryMap =
            new ConcurrentDictionary<Test, IList<IContainer>>();

        public static void Add<T>(Test parent, object key, object value) where T : new() {
            IList<IContainer> repositoryList = new List<IContainer>();
            IContainer repository = null;
            int listIndex;

            if (repositoryMap.ContainsKey(parent)) {
                repositoryList = repositoryMap[parent];
            }

            if (CheckIfContainerExists(repositoryList, (IContainer)new T())) {
                repository = GetMatchedMap(repositoryList, (IContainer)new T());
            }

            listIndex = repositoryList.IndexOf(repository);

            repository = repository ?? (IContainer)new T();

            if (repository.ContainsKey(key)) {
                return;
            }

            repository.Add(key, value);

            if (listIndex == -1) {
                repositoryList.Add(repository);
            }
            else {
                repositoryList[listIndex] = repository;
            }

            repositoryMap[parent] = repositoryList;
        }

        public static void Clear<T>(Test parent) where T : new() {
            repositoryMap[parent].Clear();
        }

        public static IContainer Get<T>(Test parent) where T : new() {
            if (!repositoryMap.ContainsKey(parent)) {
                repositoryMap[parent] = new List<IContainer>();
            }

            if (CheckIfContainerExists(repositoryMap[parent], (IContainer)new T())) {
                return GetMatchedMap(repositoryMap[parent], (IContainer)new T());
            }

            return (IContainer)new T();
        }

        private static bool CheckIfContainerExists(IList<IContainer> repositoryList, IContainer T) {
            foreach (IContainer repo in repositoryList) {
                if (repo.GetType().Equals(T.GetType())) {
                    return true;
                }
            }

            return false;
        }

        private static IContainer GetMatchedMap(IList<IContainer> repositoryList, IContainer T) {
            foreach (IContainer repo in repositoryList) {
                if (repo.GetType().Equals(T.GetType())) {
                    return repo;
                }
            }

            throw new
                   ArgumentNullException(nameof(T),
                        "Girilen IContainer değerine uygun bir eşleşme yapılamadı.");
        }

        public static Test GetParentByElement<T>(object element) where T : new() {
            Test parent = null;

            IContainer referenceContainer = (IContainer)new T();

            foreach (var repo in repositoryMap) {
                foreach (IContainer container in repo.Value) {
                    if (!container.GetType().Equals(referenceContainer.GetType())) {
                        continue;
                    }

                    foreach (var containerElement in container) {
                        if (containerElement == element) {
                            return repo.Key;
                        }
                    }
                }
            }

            return parent;
        }
    }
}

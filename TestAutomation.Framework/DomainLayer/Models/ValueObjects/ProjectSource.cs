using System;

namespace TestAutomation.Framework.DomainLayer.Models.ValueObjects
{
    /// <summary>
    /// Project source sınıfı; Proje adını almayı sağlayacak method ve propertyleri barındırır. 
    /// </summary>
    public class ProjectSource
    {
        private string projectName;

        /// <summary>
        /// ProjectName property. Proje adının null veya empty olması durumunda hata fırlatır.
        /// </summary>
        public string ProjectName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(projectName))
                    throw new ArgumentNullException(projectName, "Proje adı null olamaz.");

                return projectName;
            }

            set
            {
                projectName = value;
            }
        }


        /// <summary>
        ///  Constructor method.
        /// </summary>
        /// <param name="projectName">Null veya empty olmayan bir parametre yollanmalıdır, aksi durumda hata alır.</param>
        public ProjectSource(string projectName)
        {
            ProjectName = projectName;
        }
    }
}

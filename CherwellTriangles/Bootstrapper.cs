namespace CherwellTriangles
{
    using CherwellTriangles.Model;
    using CherwellTriangles.Resolvers;
    using CherwellTriangles.ViewModels;

    using CommonServiceLocator;

    using Unity;
    using Unity.ServiceLocation;

    public interface IBootStrapper
    {
        void Initialise();
    }

    public class Bootstrapper : IBootStrapper
    {
        private readonly IUnityContainer _container = new UnityContainer();

        public void Initialise()
        {
            this._container.RegisterInstance<IBootStrapper>(this);
            
            this.ConfigureContainer();

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(this._container));
        }

        private void ConfigureContainer()
        {
            this._container.RegisterType<ITriangleViewModel, TriangleViewModel>();
            this._container.RegisterType<ISetViewModel, SetViewModel>();
            this._container.RegisterType<ITriangleResolver, TriangleResolver>();
        }
    }
}
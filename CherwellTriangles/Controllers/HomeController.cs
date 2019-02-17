namespace CherwellTriangles.Controllers
{
    using System;
    using System.Drawing;
    using System.Web.Mvc;

    using CherwellTriangles.Model;
    using CherwellTriangles.ViewModels;

    using CommonServiceLocator;

    using Unity;

    public class HomeController : Controller
    {
        private readonly IUnityContainer _container;

        public HomeController() : this(ServiceLocator.Current.GetInstance<IUnityContainer>()) { }

        public HomeController(IUnityContainer container)
        {
            this._container = container;
        }

        public ActionResult Index()
        {
            this.ViewBag.Title = "Home Page";

            var setVm = this._container.Resolve<ISetViewModel>();
            setVm.Initialise();

            return this.View(setVm);
        }

        public JsonResult GetTriangleFromRef(char letter, int number)
        {
            var triangleVm = this._container.Resolve<ITriangleViewModel>();
            triangleVm.Initialise(letter, number);

            return this.Json(triangleVm);
        }
        
        public JsonResult GetTriangleFromCoords(int v1X, int v1Y, int v2X, int v2Y)
        {
            if (v1X - 10 != v2X && v1Y - 10 != v2Y)
                throw new Exception("Co-ordinates do not constitute a triangle");
            
            var triangleVm = this._container.Resolve<ITriangleViewModel>();
            var rightAngle = new PointF(v1X, v1Y);
            var topLeft = new PointF(v2X, v2Y);

            triangleVm.Initialise(rightAngle, topLeft);

            return this.Json(triangleVm);
        }
    }
}

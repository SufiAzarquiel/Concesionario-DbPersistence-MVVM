namespace Concesionario_DbPersistence_MVVM.model
{
    public class Car
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public enum Engine { Gasoline, Diesel, Hybrid, Electric };
        public Engine EngineType { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }
    }
}

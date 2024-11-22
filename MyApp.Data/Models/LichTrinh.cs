namespace netcorebus.Models
{
    public class LichTrinh
    {
        public string Id { get; set; }
        public Xe Xe { get; set; }
        public Diem Diem { get; set; }
        public List<TuyenDetail> Tuyen { get; set; }
        public DateTime Ngay { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace Sample
{
    [Table("OnlyTable")]
    public class TestTable
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("SomeColumn")]
        public int SomeColumn { get; set; }
    }
}
namespace WebApi.DataTransferObject
{
    public class StatusesDto
    {
        //dto esme yra atskirti galutine issiunciama data ,
        //issiusti tik tai ko reikia ,
        //nesiusti Relationships rezultatu ,
        //tik tos lenteles duomenis kuriuos norime matyti
        //Modelis tik be relationships
        public int status_id { get; set; }
        public string name { get; set; }
    }
}

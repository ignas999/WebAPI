using AutoMapper;
using WebApi.DataTransferObject;
using WebApi.Models;

namespace WebApi.Helper
{
    //naudojame tam kad siek tiek pagrazinti grazinama json ir netureti viduje null reiksmiu jungtiniu lenteliu
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            
            CreateMap<Statuses, StatusesDto>();

            //create status

            CreateMap<StatusesDto, Statuses>();

            CreateMap<Categories, CategoriesDto>();

            //when posting you have to reverse the mapping relationship , so in total you must have two
                CreateMap<CategoriesDto, Categories>();

            CreateMap<Transports, TransportsDto>();

            //For Create Method
                //A unique transportsDto for table with foreign keys
                CreateMap<TransportsDtoCreate, Transports>();

            CreateMap<Locations, LocationsDto>();

            //For Create Method
                CreateMap<LocationsDto, Locations>();

            CreateMap<Privilleges, PrivillegesDto>();

            CreateMap<Workers, WorkersDto>();

            //For Create Method
                CreateMap<WorkersDtoCreate, Workers>();



            CreateMap<Users, UsersDto>();

			CreateMap<UsersDto, Users>();

			CreateMap<Orders, OrdersDto>();

            // For create Method
            CreateMap<OrdersDtoCreate, Orders>();

            CreateMap<Repairs, RepairsDto>();

            //create
            CreateMap<RepairsDto, Repairs>();

            CreateMap<Maintenances, MaintenancesDto>();

            //For Create Method
                CreateMap<MaintenancesDtoCreate, Maintenances>();

            CreateMap<Transactions, TransactionsDto>();

            //For Create Method
                CreateMap<TransactionsDtoCreate, Transactions>();
        }
    }
}

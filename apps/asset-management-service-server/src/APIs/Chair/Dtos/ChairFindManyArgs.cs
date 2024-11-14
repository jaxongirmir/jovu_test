using AssetManagementService.APIs.Common;
using AssetManagementService.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class ChairFindManyArgs : FindManyInput<Chair, ChairWhereInput> { }

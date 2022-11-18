using Cinema.Data;
using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services;

public class Addresses : Commons<Addresses, Address>, IAddresses
{ }
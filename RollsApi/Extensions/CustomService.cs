namespace RollsApi.Extensions
{
  public static class CustomService
  {
    public static IServiceCollection CustomServices(this IServiceCollection services)
    {
      //services.AddSingleton<IEmailService, EmailService>();
      //services.AddSingleton<ISharedRepo, SharedRepo>();
      //services.AddSingleton<IInventoryRepo, InventoryRepo>();
      //services.AddSingleton<ISellerRepo, SellerRepo>();
      //services.AddSingleton<IPackerRepo, PackerRepo>();
      //services.AddSingleton<IPricingRepo, PricingRepo>();

      services.AddSingleton<ICountryRepo, CountryRepo>();
      services.AddSingleton<IBankRepo, BankRepo>();
      services.AddSingleton<IDepartmentRepo, DepartmentRepo>();
      services.AddSingleton<IDepartmentsRepo, DepartmentsRepo>();
      services.AddSingleton<IDesignationRepo, DesignationRepo>();
      services.AddSingleton<IEmployeeRepo, EmployeeRepo>();
      return services;
    }
  }
}

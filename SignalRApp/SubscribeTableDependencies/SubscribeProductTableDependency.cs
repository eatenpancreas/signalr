using SignalRApp.Hubs;
using SignalRApp.Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using ErrorEventArgs = System.IO.ErrorEventArgs;

namespace SignalRApp.SubscribeTableDependencies;

public class SubscribeProductTableDependency: ISubscribeProductTableDependency {
    private SqlTableDependency<Product> _tableDependency;
    private DashboardHub _dashboardHub;
    private IConfiguration _configuration;
    public SubscribeProductTableDependency(DashboardHub dashboardHub, IConfiguration configuration) {
        _dashboardHub = dashboardHub;
        _configuration = configuration;
    }
    
    public void SubscribeTableDependency(string connectionString) {
        _tableDependency = new SqlTableDependency<Product>(connectionString);
        _tableDependency.OnChanged += TableDependency_OnChanged;
        _tableDependency.OnError += TableDependency_OnError;
        _tableDependency.Start();
    }
    
    private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e) {
        Console.WriteLine(e.Message);
    }
    
    private void TableDependency_OnChanged(object sender, RecordChangedEventArgs<Product> e) {
        if (e.ChangeType != ChangeType.None) {
            _dashboardHub.SendProducts();
        }
    }
}
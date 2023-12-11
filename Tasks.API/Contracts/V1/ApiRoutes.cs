namespace Tasks.API.Contracts.V1
{
    public static class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";
        private const string Base = Root + "/" + Version;

        public static class Tasks
        {
            public const string GetAll = Base + "/tasks";
            public const string Get = Base + "/tasks/{id:Guid}";
            public const string Create = Base + "/tasks";
            public const string Update = Base + "/tasks/{id:Guid}";
            public const string Delete = Base + "/tasks/{id:Guid}";
        }



    }
}

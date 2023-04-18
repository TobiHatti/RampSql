namespace RampSql.Schema
{
    public abstract class RampSchema<Schema> : IRampSchema where Schema : RampSchema<Schema>
    {
        private Schema parentSchema = null;
        private List<Schema> schemaCollection = new List<Schema>();
        internal string Alias { get; set; } = null;

        public Schema this[string alias]
        {
            get
            {
                Schema schema;
                if (parentSchema is null) schema = Seek(alias);
                else schema = parentSchema.Seek(alias);
                Schema newSchema = RampSchemaData.CreateSub(schema);
                RampSchemaData.SwitchBranch(newSchema).IsBackCall = true;
                return newSchema;
            }
        }

        internal void RegisterSubSchema(Schema subSchema)
        {
            if (parentSchema is not null) parentSchema.schemaCollection.Add(subSchema);
            else schemaCollection.Add(subSchema);
        }

        internal void SetParentSchema(Schema parentSchema)
        {
            this.parentSchema = parentSchema;
        }

        public Schema Seek(string alias) => schemaCollection.Where(x => x.Alias == alias).First();
    }
}

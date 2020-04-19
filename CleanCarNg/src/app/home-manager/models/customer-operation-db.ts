export class CustomerOperationDB{
    constructor(
        public Id: number,
        public DateTime: Date,
        public Price: number,
        public OperationId: number,
        public CustomerId: number,
    ){}
}
import { CustomerOperation } from './customer-operation';

export class Customer{
    constructor(
        public Id: number,
        public FirstName: string,
        public SecondName: string,
        public Phone: string,
        public CountOperations: number,
        public SumPrice: number,
        public CustomerOperations: Array<CustomerOperation>
    ){}
}
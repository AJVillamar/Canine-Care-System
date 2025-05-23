export abstract class Mapper<Input, Output> {

    abstract mapToCreate(param: Input): Output;

    abstract mapToUpdate(param: Input): Output;

    abstract mapFromGet(param: Output): Input;

}

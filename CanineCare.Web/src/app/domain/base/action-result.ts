export interface ActionResult<T = void> {
    statusCode: number;
    data?: T | null;
    message: string;
}

export interface ApiResponse<T = null> {
    statusCode: number;
    data: T | null;
    message: string;
}
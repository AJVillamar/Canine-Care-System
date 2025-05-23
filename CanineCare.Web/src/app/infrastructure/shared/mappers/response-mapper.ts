import { ApiResponse } from "../entities/api-response";
import { ActionResult } from "@domain/base/action-result";

export class ResponseMapper {

    public mapFromActionResult<T>(response: ApiResponse<T | null>): ActionResult<T> {
        
        return {
            statusCode: response.statusCode,
            message: response.message,
            data: response.data ?? undefined
        };
        
    }
}

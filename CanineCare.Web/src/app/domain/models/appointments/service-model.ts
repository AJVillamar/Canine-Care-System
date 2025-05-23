import { ServiceDetailModel } from "./service-detail-model"
import { ServiceType } from "./service-type-model"

export interface ServiceModel {
    id?: string
    name?: string
    description?: string
    type?: ServiceType
    actions?: ServiceDetailModel[]
}

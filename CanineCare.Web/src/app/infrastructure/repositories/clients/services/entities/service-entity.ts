import { ServiceDetailEntity } from "./service-detail-entity"

export interface ServiceEntity {
    id?: string
    name?: string
    description?: string
    type?: string,
    actions?: ServiceDetailEntity[]
}
export interface CatalogItem {
    id: number,
    model: Model,
    price: number,
    supplier: Supplier,
}

export interface Supplier {
    id: number,
    name: string,
    logoUrl: string
}

export interface Model {
    id: number,
    company: string,
    name: string,
    power: number,
    gear: string,
    doorCount: number,
    seatCount: number,
    engine: string,
    color: string,
    imageUrl: string
}

export interface AddItemDTO {
    model: {
        name: string,
        power: number,
        gear: string,
        doorCount: number,
        company: string,
        seatCount: number,
        engine: string,
        color: string,
        imageUrl: string
    },
    price: number,
    supplierId: number,
}
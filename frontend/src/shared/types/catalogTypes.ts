export interface CatalogItem {
    id: number,
    model: Model,
    price: number,
    company: string,
}

export interface Model {
    id: number,
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
        seatCount: number,
        engine: string,
        color: string,
        imageUrl: string
    },
    price: number,
    company: string,
}
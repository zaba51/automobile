export interface CatalogItem {
    id: number,
    model: Model,
    price: number,
    supplier: Supplier,
    location: Location
}

export interface Location {
    id: number,
    cityName: string,
    countryName: string
}

export interface Supplier {
    id: number,
    name: string,
    logoUrl: string,
    locations: Location[]
}

export interface CarCompany {
    id: number,
    code: string,
    name: string
}

export interface Model {
    id: number,
    carCompany: CarCompany,
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
        carCompany: { name: string },
        seatCount: number,
        engine: string,
        color: string,
        imageUrl: string
    },
    price: number,
    supplierId: number,
    locationId: number 
}

export interface SupplierInfo {
    supplier: Supplier;
    catalogItems: CatalogItem[];
}
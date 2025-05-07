import type { Filters } from "../types/Filters";
import type { PaginatedResult } from "../types/PaginatedResult";
import type { Property } from "../types/Property";

const API_URL = "https://localhost:7024/api/Property";

export async function fetchProperties(filters: Filters = {}): Promise<PaginatedResult<Property>> {
    const query = new URLSearchParams();

    if (filters.name) query.append("name", filters.name);
    if (filters.address) query.append("address", filters.address);
    if (filters.minPrice) query.append("minPrice", filters.minPrice);
    if (filters.maxPrice) query.append("maxPrice", filters.maxPrice);
    if (filters.page) query.append("page", filters.page.toString());
    if (filters.pageSize) query.append("pageSize", filters.pageSize.toString());

    const res = await fetch(`${API_URL}?${query.toString()}`, {
        method: "GET",
        mode: "cors",
        headers: {
            "Content-Type": "application/json"
        }
    });

    if (!res.ok) throw new Error("Error to get properties");
    return await res.json();
}

export async function fetchPropertyById(id: string): Promise<Property> {
    const res = await fetch(`${API_URL}/${id}`, {
        method: "GET",
        mode: "cors",
        headers: {
            "Content-Type": "application/json"
        }
    });

    if (!res.ok) throw new Error("Error to get property");
    return await res.json();
}

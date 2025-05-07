import type { ChangeEvent } from "react";

interface PropertyFiltersProps {
    name: string;
    address: string;
    minPrice: string;
    maxPrice: string;
    onChange: (e: ChangeEvent<HTMLInputElement>) => void;
}

export default function PropertyFilters({
    name,
    address,
    minPrice,
    maxPrice,
    onChange,
}: PropertyFiltersProps) {
    return (
        <div className="filters">
            <input
                type="text"
                name="name"
                placeholder="search by name"
                value={name}
                onChange={onChange}
            />
            <input
                type="text"
                name="address"
                placeholder="search by address"
                value={address}
                onChange={onChange}
            />
            <input
                type="number"
                name="minPrice"
                placeholder="min price"
                value={minPrice}
                onChange={onChange}
            />
            <input
                type="number"
                name="maxPrice"
                placeholder="max price"
                value={maxPrice}
                onChange={onChange}
            />
        </div>
    );
}

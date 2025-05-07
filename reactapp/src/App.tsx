import { Routes, Route, Link } from "react-router-dom";
import { useEffect, useState, type ChangeEvent } from "react";
import { fetchProperties } from "./services/propertyService";
import type { Property } from "./types/Property";
import PropertyFilters from "./components/PropertyFilters";
import PropertyDetails from "./pages/PropertyDetails";
import { Pagination } from "./components/Pagination";

function HomePage() {
    const [filtered, setFiltered] = useState<Property[]>([]);
    const [loading, setLoading] = useState(true);
    const [filters, setFilters] = useState({
        name: "",
        address: "",
        minPrice: "",
        maxPrice: "",
    });
    const [page, setPage] = useState(1);
    const pageSize = 6; 
    const [totalCount, setTotalCount] = useState(0);

    useEffect(() => {
      
        if (
            filters.minPrice &&
            filters.maxPrice &&
            parseFloat(filters.minPrice) > parseFloat(filters.maxPrice)
        ) {
            setFiltered([]);
            return;
        }

        fetchProperties({ ...filters, page, pageSize })
            .then((data) => {
                setFiltered(data.items); 
                setTotalCount(data.totalCount);
            })
            .catch(console.error)
            .finally(() => setLoading(false));
    }, [filters, page]); 

    const handleFilterChange = (e: ChangeEvent<HTMLInputElement>) => {
        setFilters({ ...filters, [e.target.name]: e.target.value });
        setPage(1);
    };

    return (
        <div className="container">
            <h1 className="title">Million Real State Properties</h1>
            <PropertyFilters {...filters} onChange={handleFilterChange} />
            {loading ? (
                <p>Cargando...</p>
            ) : (
                <ul className="property-list">
                    {filtered.map((prop) => (
                        <li key={prop.id} className="property-card">
                            <img src={prop.imageUrl} alt={prop.name} />
                            <h2>{prop.name}</h2>
                            <p>{prop.address}</p>
                            <p>Precio: ${prop.price.toLocaleString()}</p>
                            <Link to={`/property/${prop.id}`}>Ver detalles</Link>
                        </li>
                    ))}
                </ul>
            )}
            <Pagination
                currentPage={page}
                totalPages={Math.ceil(totalCount / pageSize)}
                onPageChange={(p) => setPage(p)}
            />
        </div>
    );
}

function App() {
    return (
        <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/property/:id" element={<PropertyDetails />} />
        </Routes>
    );
}

export default App;

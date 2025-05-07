import { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import "../styles/PropertyDetails.css";
import type { Property } from "../types/Property";
import { fetchPropertyById } from "../services/propertyService";

const PropertyDetails = () => {
    const { id } = useParams<{ id: string }>();
    const [property, setProperty] = useState<Property | null>(null);

    useEffect(() => {
        if (id) {
            fetchPropertyById(id)
                .then(setProperty)
                .catch(console.error);
        }
    }, [id]);

    if (!property) return <p className="loading">loading property...</p>;

    return (
        <div className="details-container">
            <div className="details-card">
                <img className="details-image" src={property.imageUrl} alt={property.name} />
                <div className="details-info">
                    <h2>{property.name}</h2>
                    <p><strong>Address:</strong> {property.address}</p>
                    <p><strong>Price:</strong> ${property.price.toLocaleString()}</p>
                </div>
            </div>
            <Link to="/" className="back-link">← Back to the list</Link>
        </div>
    );
};

export default PropertyDetails;

import { useEffect, useState, useMemo } from "react";

// API JSON extract
interface ApiHairdresser {
    salonName: string | null;
    website: string | null;
    lat: number | null; // Will be "distance" later
    lng: number | null; // Will be "distance" later
    treatments?: {
        treatmentType: string | null,
        treatmentPrice: number | null
    }[] | null;
}

// Component output format
export interface HairdresserView {
    salonName: string | null;
    website: string | null;
    distance: number | null;    // Using lng as placeholder
    //price: number | null;       // Lowest treatmentPrice
    treatments?: {
        treatmentType: string | null,
        treatmentPrice: number | null
    }[] | null;
}

function toView(h: ApiHairdresser): HairdresserView {
    const distance = h.lng ?? null; // Set lng to distance placeholder

    // Find lowest price across treatments
    //const price = h.treatments && h.treatments.length ? h.treatments
    //    .map(t => t?.treatmentPrice ?? null)
    //    .filter((n): n is number => n !== null)
    //    .reduce<number | null>((min, n) => (min === null ? n : Math.min(min, n)), null) : null;


    //const treatments = h.treatments && h.treatments.length ? h.treatments
    //    .map(t => t?.treatmentType ?? null)
    //    .filter((name): name is string => !!name)   // Removing null/undefined/empty
    //    : null;
    
    return {
        salonName: h.salonName,
        website: h.website ?? null,
        distance,
        treatments: h.treatments ?? null,
    };
}

export default function useHairdresserSearch(address: string) {
    const [data, setData] = useState<HairdresserView[] | null>(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (!address?.trim()) return;

        const ctrl = new AbortController();

        (async () => {
            try {
                setLoading(true);
                setError(null);

                const res = await fetch(
                    `https://localhost:7001/api/Hairdressers/Search?address=${encodeURIComponent(address)}`,
                    {
                        method: "POST",
                        signal: ctrl.signal,
                    }
                );

                if (!res.ok) throw new Error(`HTTP ${res.status}`);

                const json = (await res.json()) as {
                    coordinates?: { lat: number | null; lon: number | null };
                    hairdressers?: ApiHairdresser[];
                };

                const views = (json.hairdressers ?? [])
                    .map(toView)
                    .sort((a, b) => (a.distance ?? Infinity) - (b.distance ?? Infinity));
                setData(views);

            } catch (e: unknown) {
                if (e instanceof DOMException && e.name === "AbortError") return;
                setError(e instanceof Error ? e.message : String(e));
            } finally {
                setLoading(false);
            }
        })();

        return () => ctrl.abort();
    }, [address]);

    // Extract list of unique treatments
    const allTreatments = useMemo(() => {
        return (
            data?.flatMap(h => h.treatments ?? [])
                .filter(t => !!t.treatmentType)
                .map((t) => t.treatmentType as string)
                .filter((t, i, arr) => arr.indexOf(t) === i)
            ?? []
        );
    }, [data]);

    return { data, allTreatments, loading, error };
}
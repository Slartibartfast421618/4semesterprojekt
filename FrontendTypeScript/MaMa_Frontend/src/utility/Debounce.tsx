import { useEffect, useRef, useState } from "react";

export function useRequestGate<T>(value: T): T {
    const DEBOUNCE_MS = 500;        // Time to wait after keypress
    const MIN_INTERVAL_MS = 1000;   // Time to wait after latest request

    const [gatedValue, setGatedValue] = useState(value);
    const lastFireRef = useRef<number>(0);
    const debounceIdRef = useRef<number | null>(null);
    const minWaitIdRef = useRef<number | null>(null);

    useEffect(() => {
        // Clear any in-flight timers before scheduling new ones
        if (debounceIdRef.current) clearTimeout(debounceIdRef.current);
        if (minWaitIdRef.current) clearTimeout(minWaitIdRef.current);

        const now = Date.now();
        const sinceLast = now - lastFireRef.current;
        const waitForMin = Math.max(0, MIN_INTERVAL_MS - sinceLast);

        // Standard debounce
        debounceIdRef.current = window.setTimeout(() => {
            // Enforce minimum interval since last fire
            minWaitIdRef.current = window.setTimeout(() => {
                setGatedValue(value);
                lastFireRef.current = Date.now();
            }, waitForMin);
        }, DEBOUNCE_MS);

        // Cleanup for next change/unmount
        return () => {
            if (debounceIdRef.current) clearTimeout(debounceIdRef.current);
            if (minWaitIdRef.current) clearTimeout(minWaitIdRef.current);
        };
    }, [value]);

    return gatedValue;
}
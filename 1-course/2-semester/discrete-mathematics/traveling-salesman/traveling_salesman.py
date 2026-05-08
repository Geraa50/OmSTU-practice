import sys
from itertools import permutations
from copy import deepcopy

if hasattr(sys.stdout, "reconfigure"):
    sys.stdout.reconfigure(encoding="utf-8")

INF = float("inf")

C = [
    [INF, 41,  3,  85, 53],
    [68,  INF, 47, 28, 25],
    [75,  58,  INF,62, 81],
    [17,  47,  69, INF, 8],
    [41,  25,  63, 69, INF],
]
N = len(C)


def reduce_matrix(M, rows, cols):
    total = 0
    for i in rows:
        vals = [M[i][j] for j in cols if M[i][j] != INF]
        if not vals:
            continue
        m = min(vals)
        if m > 0:
            for j in cols:
                if M[i][j] != INF:
                    M[i][j] -= m
            total += m
    for j in cols:
        vals = [M[i][j] for i in rows if M[i][j] != INF]
        if not vals:
            continue
        m = min(vals)
        if m > 0:
            for i in rows:
                if M[i][j] != INF:
                    M[i][j] -= m
            total += m
    return total


def zero_penalty(M, rows, cols, i, j):
    row_vals = [M[i][k] for k in cols if k != j and M[i][k] != INF]
    col_vals = [M[k][j] for k in rows if k != i and M[k][j] != INF]
    rm = min(row_vals) if row_vals else 0
    cm = min(col_vals) if col_vals else 0
    return rm + cm


def find_branch_edge(M, rows, cols):
    best_penalty = -1
    best_edge = None
    for i in rows:
        for j in cols:
            if M[i][j] == 0:
                p = zero_penalty(M, rows, cols, i, j)
                if p > best_penalty:
                    best_penalty = p
                    best_edge = (i, j)
    return best_edge, best_penalty


def forbid_closing_edge(M, edges, i, j):
    succ = {a: b for a, b in edges}
    pred = {b: a for a, b in edges}
    end = j
    while end in succ:
        end = succ[end]
    start = i
    while start in pred:
        start = pred[start]
    if end != start and M[end][start] != INF:
        M[end][start] = INF


def tour_is_complete(edges):
    if len(edges) != N:
        return False
    succ = {a: b for a, b in edges}
    cur = 0
    visited = {0}
    for _ in range(N - 1):
        if cur not in succ:
            return False
        cur = succ[cur]
        if cur in visited:
            return False
        visited.add(cur)
    return cur in succ and succ[cur] == 0


def branch_and_bound():
    best_cost = [INF]
    best_edges = [None]
    log = []

    def recurse(M, edges, lb, depth):
        rows = [i for i in range(N) if any(M[i][j] != INF for j in range(N))]
        cols = [j for j in range(N) if any(M[i][j] != INF for i in range(N))]

        log.append({
            "depth": depth, "edges": list(edges),
            "lb": lb, "rows": rows[:], "cols": cols[:],
        })

        if not rows or not cols:
            if len(edges) == N and tour_is_complete(edges) and lb < best_cost[0]:
                best_cost[0] = lb
                best_edges[0] = list(edges)
            return

        edge, _ = find_branch_edge(M, rows, cols)
        if edge is None:
            return
        i, j = edge

        M_in = deepcopy(M)
        new_edges = edges + [(i, j)]
        for k in range(N):
            M_in[i][k] = INF
            M_in[k][j] = INF
        forbid_closing_edge(M_in, new_edges, i, j)
        rows_in = [r for r in rows if r != i]
        cols_in = [c for c in cols if c != j]
        add = reduce_matrix(M_in, rows_in, cols_in)
        new_lb = lb + add

        if len(new_edges) == N - 1:
            remaining_row = [r for r in rows_in]
            remaining_col = [c for c in cols_in]
            if len(remaining_row) == 1 and len(remaining_col) == 1:
                a, b = remaining_row[0], remaining_col[0]
                if C[a][b] != INF:
                    final_edges = new_edges + [(a, b)]
                    if tour_is_complete(final_edges):
                        total = sum(C[x][y] for x, y in final_edges)
                        if total < best_cost[0]:
                            best_cost[0] = total
                            best_edges[0] = final_edges
            return

        if new_lb < best_cost[0]:
            recurse(M_in, new_edges, new_lb, depth + 1)

        M_ex = deepcopy(M)
        M_ex[i][j] = INF
        add = reduce_matrix(M_ex, rows, cols)
        new_lb = lb + add
        if new_lb < best_cost[0]:
            recurse(M_ex, list(edges), new_lb, depth + 1)

    M0 = deepcopy(C)
    lb0 = reduce_matrix(M0, list(range(N)), list(range(N)))
    recurse(M0, [], lb0, 0)
    return best_cost[0], best_edges[0], log


def edges_to_tour(edges, start=0):
    succ = {a: b for a, b in edges}
    tour = [start]
    cur = start
    for _ in range(len(edges)):
        cur = succ[cur]
        tour.append(cur)
    return tour


# ---------------------------------------------------------------------------
# полный перебор для проверки что всё одинаково
# ---------------------------------------------------------------------------

def brute_force():
    best = (INF, None)
    for perm in permutations(range(1, N)):
        path = [0] + list(perm) + [0]
        cost = sum(C[path[i]][path[i + 1]] for i in range(len(path) - 1))
        if cost < best[0]:
            best = (cost, path)
    return best



if __name__ == "__main__":
    cost, edges, _ = branch_and_bound()
    tour = edges_to_tour(edges)
    print("-".join(str(x + 1) for x in tour))
    print(cost)

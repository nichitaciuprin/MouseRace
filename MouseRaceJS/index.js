let mouseX = 0;
let mouseY = 0;

document.addEventListener('mousemove', (event) => { mouseX = event.clientX; mouseY = event.clientY });

function getWindowRect() {
    var p0 = new Vector2(0, 0);
    var p1 = new Vector2(window.innerWidth, window.innerHeight);
    return new Rect(p0, p1)
}

function clear() {
    while (document.body.firstChild)
        document.body.removeChild(document.body.firstChild);
}

function drawCircle(x, y, radius) {
    const circle = document.createElement('div');
    circle.style.width = `${radius * 2}px`;
    circle.style.height = `${radius * 2}px`;
    circle.style.borderRadius = '50%';
    circle.style.backgroundColor = 'black';
    circle.style.position = 'absolute';
    circle.style.left = `${x - radius}px`;
    circle.style.top = `${y - radius}px`;
    document.body.appendChild(circle);
}
function drawRect(x1, x2, y1, y2, color) {
    const rect = document.createElement('div');
    rect.style.position = 'absolute';
    rect.style.left = x1 + 'px';
    rect.style.top = y1 + 'px';
    rect.style.width = (x2 - x1) + 'px';
    rect.style.height = (y2 - y1) + 'px';
    rect.style.backgroundColor = color;
    document.body.appendChild(rect);
}

function drawButton(x1, y1, text, callback, shouldRemove = false) {
    const button = document.createElement('button');
    button.style.position = 'absolute';
    button.style.left = x1 + 'px';
    button.style.top = y1 + 'px';
    button.style.transform = 'translate(-50%, -50%)';
    button.textContent = text;
    button.addEventListener('click', () => {
        callback();

        if (shouldRemove) {
            document.body.removeChild(button)
        }
    });
    document.body.appendChild(button);
}


class Helper {
    static range(min, max) { return min + ((max - min) * Math.random()); }
}

class Vector2 {
    x;
    y;
    constructor(x, y) {
        this.x = x;
        this.y = y;
    }
    static zero() { return new Vector2(0, 0); }
    static equals(a, b) { return a.x == b.x && a.y == b.y; }
    static add(a, b) { return new Vector2(a.x + b.x, a.y + b.y); }
    static sub(a, b) { return new Vector2(a.x - b.x, a.y - b.y); }
    static div(a, value) { return new Vector2(a.x / value, a.y / value) }
    static mul(a, value) { return new Vector2(a.x * value, a.y * value); }
    static moveTowards(fromVec, toVec, delta) {
        if (Vector2.equals(fromVec, toVec)) return fromVec;
        let diff = Vector2.sub(toVec, fromVec);
        let dist = diff.magnitude();
        if (dist <= delta) return toVec;
        let dir = diff.normalized();
        let moveVec = Vector2.mul(dir, delta);
        return Vector2.add(fromVec, moveVec);
    }
    static distance(v1, v2) {
        var diff = Vector2.sub(v1, v2);
        return diff.magnitude();
    }
    magnitude() {
        var distSquared = this.x * this.x + this.y * this.y;
        var dist = Math.sqrt(distSquared);
        return dist;
    }
    lengthSquared() {
        var distSquared = this.x * this.x + this.y * this.y;
        return distSquared;
    }
    normalized() {
        let dist = this.magnitude();
        let t = 1.0 / dist;
        return new Vector2(this.x * t, this.y * t);
    }
    static randDirection() {
        let x = Helper.range(-1, 1);
        let y = Helper.range(-1, 1);
        let vec = new Vector2(x, y);
        return vec.normalized();
    }
}

class Rect {
    p0;
    p1;
    constructor(p0, p1) {
        this.p0 = p0;
        this.p1 = p1;
    }
    minX() { return Math.min(this.p0.x, this.p1.x); }
    maxX() { return Math.max(this.p0.x, this.p1.x); }
    minY() { return Math.min(this.p0.y, this.p1.y); }
    maxY() { return Math.max(this.p0.y, this.p1.y); }
    center() { return Vector2.div(Vector2.add(this.p0, this.p1), 2); }
    width() { maxX() - minX(); }
    height() { maxY() - minY(); }
    move(offset) { return new Rect(p0 + offset, p1 + offset); }
    randPointInside() {
        var x = Helper.range(this.minX(), this.maxX());
        var y = Helper.range(this.minY(), this.maxY());
        return new Vector2(x, y);
    }
    draw(color) {
        drawRect(this.p0.x, this.p1.x, this.p0.y, this.p1.y, color);
    }
}

class Cyrcle {
    center;
    radius;
    constructor(center, radius) {
        this.center = center;
        this.radius = radius;
    }
    outerRect() {
        var p0 = new Vector2(this.center.x, this.center.y);
        var p1 = new Vector2(this.center.x, this.center.y);
        p0.x -= this.radius;
        p0.y -= this.radius;
        p1.x += this.radius;
        p1.y += this.radius;
        return new Rect(p0, p1);
    }
    draw() {
        drawCircle(this.center.x, this.center.y, this.radius);
    }
}

class Physics {
    static insideBoundOffsetCyrcle(bound, cyrcle) {
        var rect = cyrcle.outerRect();
        return this.insideBoundOffsetRect(bound, rect);
    }

    static insideBoundOffsetRect(bound, rect) {
        var xOffset = 0;
        var yOffset = 0;
        if (bound.maxX() < rect.maxY()) xOffset = bound.maxY() - rect.maxY();
        else if (bound.minX() > rect.minX()) xOffset = bound.minX() - rect.minX();
        if (bound.maxY() < rect.maxY()) yOffset = bound.maxY() - rect.maxY();
        else if (bound.minY() > rect.minY()) yOffset = bound.minY() - rect.minY();
        return new Vector2(xOffset, yOffset);
    }

    static insideBoundCyrcle(bound, cyrcle) {
        var rect = cyrcle.outerRect();
        return InsideBound(bound, rect);
    }

    static insideBoundRect(bound, rect) {
        if (bound.maxX < rect.maxX) return false;
        else if (bound.minX > rect.minX) return false;
        if (bound.maxY < rect.maxY) return false;
        else if (bound.minY > rect.minY) return false;
        return true;
    }

    static collisionPointCyrcle(point, cyrcle) {
        return Vector2.distance(point, cyrcle.center) < cyrcle.radius;
    }

    static collisionPointRect(point, rect) {
        if (point.x < rect.minX()) return false;
        if (point.x > rect.maxX()) return false;
        if (point.y < rect.minY()) return false;
        if (point.y > rect.maxY()) return false;
        return true;
    }
    static collisionCyrcleCyrcle(a, b) {
        var dist1 = Vector2.distance(a.center, a.center);
        var dist2 = a.radius + b.radius;
        return dist1 < dist2;
    }
}

class MyElement {
    position = 0;
    size = 0;
    speed = 0;
    constructor(game) {
        this.position = game.bound.randPointInside();
        this.size = Helper.range(10, 20);
        this.speed = Helper.range(10, 200);
    }
}

class Chase extends MyElement {
    constructor(game) {
        super(game);
    }
    update(deltaTime, game) {
        if (this.mouseIntersect(game)) {
            this.onHitTarget(game);
            return;
        }
        this.position = Vector2.moveTowards(this.position, game.mousePosition, this.speed * deltaTime);
    }

    render() {
        this.getShape().draw("red");
    }

    mouseIntersect(game) {
        return Physics.collisionPointRect(game.mousePosition, this.getShape());
    }

    onHitTarget(game) {
        game.endGame();
    }

    getShape() {
        let p0 = new Vector2(this.position.x, this.position.y);
        let p1 = new Vector2(this.position.x, this.position.y);
        p0.x -= this.size * 2;
        p0.y -= this.size;
        p1.x += this.size * 2;
        p1.y += this.size;
        return new Rect(p0, p1);
    }
}

class Escape extends MyElement {
    constructor(game) {
        super(game);
    }
    update(deltaTime, game) {
        if (this.mouseIntersect(game)) {
            this.onHitTarget(game);
            return;
        }

        let dir = Vector2.sub(this.position, game.mousePosition).normalized();
        dir = Vector2.mul(dir, this.speed);
        dir = Vector2.mul(dir, deltaTime);
        this.position = Vector2.add(this.position, dir);
        let offset = Physics.insideBoundOffsetRect(game.bound, this.getShape());
        this.position = Vector2.add(this.position, offset);
    }

    render() {
        let shape = this.getShape();
        shape.draw("green");
    }

    mouseIntersect(game) {
        return Physics.collisionPointRect(game.mousePosition, this.getShape());
    }

    onHitTarget(game) {
        game.addExtraScore(5);
        let duno = game.bound.randPointInside();
        this.position = duno
    }

    getShape() {
        let p0 = new Vector2(this.position.x, this.position.y);
        let p1 = new Vector2(this.position.x, this.position.y);
        p0.x -= this.size;
        p0.y -= this.size;
        p1.x += this.size;
        p1.y += this.size;
        return new Rect(p0, p1);
    }
}

class Random extends MyElement {
    constructor(game) {
        super(game);
        this.changeDirection();
    }

    update(deltaTime, game) {
        if (this.mouseIntersect(game)) {
            this.onHitTarget(game);
            return;
        }
        let temp = Vector2.mul(this.direction, this.speed);
        temp = Vector2.mul(temp, deltaTime);
        this.position = Vector2.add(this.position, temp);
        let offset = Physics.insideBoundOffsetCyrcle(game.bound, this.getShape());
        if (Vector2.equals(offset, Vector2.zero())) return;
        this.position = Vector2.add(this.position, offset);
        this.changeDirection();

        // reduse shaking
        offset = Vector2.mul(offset.normalized(), 5);
        this.position = Vector2.add(this.position, offset);
    }

    render() {
        this.getShape().draw("yellow");
    }

    mouseIntersect(game) {
        return Physics.collisionPointCyrcle(game.mousePosition, this.getShape());
    }

    onHitTarget(game) {
        game.endGame();
    }

    getShape() {
        var center = new Vector2(this.position.x, this.position.y);
        return new Cyrcle(center, this.size);
    }

    changeDirection() {
        this.direction = Vector2.randDirection();
    }
}
const GameState =
{
    Start: 'Start',
    Play: 'Play',
    GameOver: 'GameOver',
};
class Game {
    state = GameState.Start;
    elements = [];
    score = 0
    bound;
    mousePosition;
    constructor(bound) {
        this.bound = bound;
        this.mousePosition = Vector2.zero();
    }
    update(deltaTime) {
        clear();
        switch (this.state) {
            case GameState.Play:
                {
                    this.score += deltaTime;
                    this.elements.forEach(element => {
                        element.update(deltaTime, this);
                        element.render();
                    });
                }
        }
        drawButton(100, 100, parseInt(this.score), _ => { });
    }

    start() {
        if (this.state != GameState.Start) return;
        this.state = GameState.Play;
        this.elements =
            [
                new Chase(this),
                new Chase(this),
                new Chase(this),
                new Chase(this),
                new Random(this),
                new Random(this),
                new Random(this),
                new Random(this),
                new Escape(this),
                new Escape(this),
                new Escape(this),
                new Escape(this),
            ];

        setInterval(() => {
            this.setBound(getWindowRect());
            this.setMousePosition(new Vector2(mouseX, mouseY));
            this.update(0.020);
        }, 20);

    }
    endGame() {
        this.state = GameState.GameOver;
        alert(`Game Over. Your score is: ${parseInt(this.score)}`)
        location.reload();
    }

    addExtraScore(value) {
        if (value <= 0) return;
        this.score += value;
    }

    setMousePosition(value) {
        this.mousePosition = new Vector2(value.x, value.y);
    }

    setBound(bound) {
        this.bound = bound;
    }
}

let game = new Game(getWindowRect());

const startGame = () => {
    game.start();
    setInterval(() => {
        game.setBound(getWindowRect());
        game.setMousePosition(new Vector2(mouseX, mouseY));
        game.update(0.020);
    }, 20);
}

drawButton(100, 100, 'Start', startGame)
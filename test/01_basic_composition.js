
const {expect}  = require('chai')
const {stream} = require('flyd')

const add_pen = (msg='') => msg + "pen"
const add_apple = (msg='') => msg + "Apple"
const add_space= (msg='') => msg + ' '

const comp1 = (f, g) => x => f(g(x))


const comp = (...funs) => x => funs.reduceRight((y, f)=> f(y), x)

const inc = x => x + 1
const dup = x => x * 2

const Composition = x => ({
  map: f => Composition(f(x)), //then, pipe
  return: () => x,
  done: f => f(x)
})

Composition.of =x=>Composition(x)


describe('Composition', () => {
  describe('Pointless compostion', () => {
    it('Basic', () =>{
      const apple_pen = comp1(add_pen, add_apple)
      expect(apple_pen()).to.equal('Applepen');
    })
    it('Basic tree functions compostion',
      ()=>{
        const apple_pen = comp1(add_pen, comp1(add_space, add_apple))
        expect(apple_pen()).to.equal('Apple pen');
      })
    it('Normal comp', ()=> {
      expect(comp(add_pen, add_space, add_apple)('')).to.equal('Apple pen');
    })
  })

  describe('Algebraic compostion', ()=>{
    it('Identity with return', () => {
      const ap_comp = Composition.of()
                        .map(add_apple)
                        .map(add_space)
                        .map(add_pen)
      expect(ap_comp.return(),'Apple pen')
    })

    it('Identity without context exit', () => {
        Composition.of()
            .map(add_apple)
            .map(add_space)
            .map(add_pen)
            .done(x => expect(x).to.be.equal('Apple pen'))
    })

    it('Numeric example', () => {
        Composition.of(5)
            .map(inc)
            .map(dup)
            .map(inc)
            .done(x => expect(x).to.be.equal(13))
    })
    it('Array Composition', () => {
      Array.prototype.done = function (cb) {cb(this)};
        Array.of(5)
            .map(inc)
            .map(dup)
            .map(inc)
            .done(x => expect(x).to.deep.equal([13]))
    })
    it('Stream Composition',()=>{
      var s= stream(5)
            .map(inc)
            .map(dup)
            .map(inc)
      expect(s()).to.be.equal(13)

    })
  })
})

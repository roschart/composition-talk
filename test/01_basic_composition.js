
const {expect} = require('chai')
const {stream} = require('flyd')

const addPen = (msg = '') => msg + 'pen'
const addApple = (msg = '') => msg + 'Apple'
const addSpace = (msg = '') => msg + ' '

const comp1 = (f, g) => x => f(g(x))
const comp = (...funs) => x => funs.reduceRight((y, f) => f(y), x)

const inc = x => x + 1
const dup = x => x * 2

const Composition = x => ({
  map: f => Composition(f(x)), // then, pipe, select
  return: () => x,
  done: f => f(x)
})

Composition.of = x => Composition(x)

describe('Composition', () => {
  describe('Pointless compostion', () => {
    it('Basic', () => {
      const applePen = comp1(addPen, addApple)
      expect(applePen()).to.equal('Applepen')
    })
    it('Basic tree functions compostion',
      () => {
        const applePen = comp1(addPen, comp1(addSpace, addApple))
        expect(applePen()).to.equal('Apple pen')
      })
    it('Normal comp', () => {
      expect(comp(addPen, addSpace, addApple)('')).to.equal('Apple pen')
    })
  })

  describe('Algebraic compostion', () => {
    it('Identity with return', () => {
      const apComp = Composition.of()
                        .map(addApple)
                        .map(addSpace)
                        .map(addPen)
      expect(apComp.return(), 'Apple pen')
    })

    it('Identity without context exit', () => {
      Composition.of()
          .map(addApple)
          .map(addSpace)
          .map(addPen)
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
      Array.prototype.done = function (cb) { cb(this) }  // eslint-disable-line
      Array.of(5)
          .map(inc)
          .map(dup)
          .map(inc)
          .done(x => expect(x).to.deep.equal([13]))
    })
    it('Stream Composition', () => {
      var s = stream(5)
            .map(inc)
            .map(dup)
            .map(inc)
      expect(s()).to.be.equal(13)
    })
  })
})

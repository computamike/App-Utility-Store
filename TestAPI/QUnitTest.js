// Look at this page : https://github.com/alexbeletsky/rest.mvc.example/blob/master/src/Web/Scripts/Tests/api/tests.api.js - 
// might offer interesting approach to instantiating Ajax calls to REST API

QUnit.test("hello test", function (assert) {
    assert.ok(1 == "1", "Passed!");
});

QUnit.test("Get All Products", function (assert)
{
    function square(x) {
        return x * x;
    }

    var result = square(2);

    assert.equal(result, 4, "square(2) equals 4");

});

